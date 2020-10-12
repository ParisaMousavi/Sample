provider "azurerm" {
  # Whilst version is optional, we /strongly recommend/ using it to pin the version of the Provider being used
  version = "=2.20.0"
  features {}
}

variable "image-tag" {
  type = string
  description = "The Build-Id is the image tag."
}

terraform{
    backend "azurerm" {
        resource_group_name = "sample-terraform-rg"
        storage_account_name = "sampleterraform"
        container_name = "terrform"
        key = "development.terraform.tfstate"
    }
}

data "azurerm_client_config" "current" {}

resource "azurerm_resource_group" "rg" {
    name = "azure-sample-rg"
    location = "West Europe"

    tags = {
        environment = "staging",
        project = "sample"
    }
}


data "azurerm_container_registry" "azure-sample-acr" {
  name                = "azuresampleacr"
  resource_group_name = "azure-sample-rg"
}

data "azurerm_cosmosdb_account" "sample-cosmosdb" {
  name                = "sample-cosmosdb-sql-weu"
  resource_group_name = "sample-rg"
}


resource "azurerm_container_group" "azure-sample-aci" {
  name                = "azure-sample"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  ip_address_type     = "public"
  dns_name_label      = "azure-sample"
  os_type             = "Windows"

  image_registry_credential {
    server   = data.azurerm_container_registry.azure-sample-acr.login_server
    username = data.azurerm_container_registry.azure-sample-acr.admin_username
    password = data.azurerm_container_registry.azure-sample-acr.admin_password 
  }

  container {
    name   = "products"
    image  = "${data.azurerm_container_registry.azure-sample-acr.login_server}/sampleapiproducts:${var.image-tag}"
    cpu    = "0.5"
    memory = "1.5"
    ports {
      port     = 80
      protocol = "TCP"
    }
    secure_environment_variables = {
      DBUri = data.azurerm_cosmosdb_account.sample-cosmosdb.endpoint,
      DBKey =  data.azurerm_cosmosdb_account.sample-cosmosdb.primary_master_key
      ProductDBConnectionString = "Server=tcp:${azurerm_sql_server.products-db-srv.fully_qualified_domain_name},1433;Initial Catalog=${azurerm_sql_database.products-db.name};Persist Security Info=False;User ID=${azurerm_sql_server.products-db-srv.administrator_login};Password=${azurerm_sql_server.products-db-srv.administrator_login_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
      }
    }  

  container {
    name   = "orders"
    image  = "${data.azurerm_container_registry.azure-sample-acr.login_server}/sampleapiorders:${var.image-tag}"
    cpu    = "0.5"
    memory = "1.5"
    ports {
      port     = 80
      protocol = "TCP"
    }
    secure_environment_variables = {
      OrderDBConnectionString = "Server=tcp:${azurerm_sql_server.products-db-srv.fully_qualified_domain_name},1433;Initial Catalog=${azurerm_sql_database.products-db.name};Persist Security Info=False;User ID=${azurerm_sql_server.products-db-srv.administrator_login};Password=${azurerm_sql_server.products-db-srv.administrator_login_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
      }    
    }
  }


resource "azurerm_storage_account" "storage" {
  name                     = "azuresampleimagestorage"
  resource_group_name      = azurerm_resource_group.rg.name
  location                 = azurerm_resource_group.rg.location
  account_tier             = "Standard"
  account_replication_type = "LRS"

  static_website {
    index_document = "products.html"
  }

  tags = {
    environment = "staging",
    project = "sample"
  }
}


resource "azurerm_sql_server" "products-db-srv" {
  name                         = "products-db-srv"
  resource_group_name          = azurerm_resource_group.rg.name
  location                     = azurerm_resource_group.rg.location
  version                      = "12.0"
  administrator_login          = "azureuser"
  administrator_login_password = "P@risa2018#1"

  tags = {
    environment = "staging",
    project = "sample"
  }
}

resource "azurerm_sql_database" "products-db" {
  name                  = "products"
  resource_group_name   = azurerm_resource_group.rg.name
  location              = azurerm_resource_group.rg.location
  server_name           = azurerm_sql_server.products-db-srv.name
  collation             = "SQL_Latin1_General_CP1_CI_AS"
  create_mode           = "Default"


  tags = {
    environment = "staging",
    project = "sample"
  }
}

