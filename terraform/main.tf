provider "azurerm" {
  # Whilst version is optional, we /strongly recommend/ using it to pin the version of the Provider being used
  version = "=2.20.0"
  features {}
}

variable "db_uri" {
  type = string
}

variable "db_key" {
  type = string
}

variable "acr_user" {
  type = string
}

variable "acr_pass" {
  type = string
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


resource "azurerm_resource_group" "vote-resource-group" {
  name     = "vote-resource-group"
  location = "westus"
}

resource "random_integer" "ri" {
  min = 10000
  max = 99999
}

resource "azurerm_container_group" "vote-aci" {
  name                = "vote-aci-parisa"
  location            = azurerm_resource_group.vote-resource-group.location
  resource_group_name = azurerm_resource_group.vote-resource-group.name
  ip_address_type     = "public"
  dns_name_label      = "vote-aci-parisa"
  os_type             = "linux"

  container {
    name   = "vote-aci-parisa"
    image  = "microsoft/azure-vote-front:cosmosdb"
    cpu    = "0.5"
    memory = "1.5"
    ports {
      port     = 80
      protocol = "TCP"
    }

    secure_environment_variables = {
      "COSMOS_DB_ENDPOINT"  = azurerm_cosmosdb_account.vote-cosmos-db.endpoint
      "COSMOS_DB_MASTERKEY" = azurerm_cosmosdb_account.vote-cosmos-db.primary_master_key
      "TITLE"               = "Azure Voting App"
      "VOTE1VALUE"          = "Cats"
      "VOTE2VALUE"          = "Dogs"
    }
  }
}


data "azurerm_container_registry" "azure-sample-acr" {
  name                = "azuresampleacr"
  resource_group_name = "azure-sample-rg"
}

data "azurerm_cosmosdb_account" "azure-sample-cosmosdb-account" {
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
    name   = "azure-sample"
    image  = "${data.azurerm_container_registry.azure-sample-acr.login_server}/sampleapiproducts:229"
    cpu    = "0.5"
    memory = "1.5"
    ports {
      port     = 80
      protocol = "TCP"
    }
    secure_environment_variables = {
      DBUri = data.azurerm_cosmosdb_account.azure-sample-cosmosdb-account.endpoint,
      DBKey =  data.azurerm_cosmosdb_account.azure-sample-cosmosdb-account.primary_key,
    }    
  }
}


resource "azurerm_cosmosdb_account" "vote-cosmos-db" {
  name                = "tfex-cosmos-db-${random_integer.ri.result}"
  location            = azurerm_resource_group.vote-resource-group.location
  resource_group_name = azurerm_resource_group.vote-resource-group.name
  offer_type          = "Standard"
  kind                = "GlobalDocumentDB"

  consistency_policy {
    consistency_level       = "BoundedStaleness"
    max_interval_in_seconds = 10
    max_staleness_prefix    = 200
  }

  geo_location {
    location          = "westus"
    failover_priority = 0
  }
}


resource "azurerm_storage_account" "storage" {
  name                     = "azuresampleimagestorage"
  resource_group_name      = azurerm_resource_group.rg.name
  location                 = azurerm_resource_group.rg.location
  account_tier             = "Standard"
  account_replication_type = "LRS"

  tags = {
    environment = "staging",
    project = "sample"
  }
}

output "dns" {
  value = azurerm_container_group.vote-aci.fqdn
}
