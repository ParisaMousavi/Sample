provider "azurerm" {
  # Whilst version is optional, we /strongly recommend/ using it to pin the version of the Provider being used
  version = "=2.20.0"
  features {}
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