terraform{
    backend "remote" {
      organization = "parisa-training"
      workspaces {
          name = "sample-project"
      }
    }
}

provider "azurerm" {
    subscription_id = var.subscription_id
    tenant_id = var.tenant_id
    client_id = var.client_id
    client_secret = var.client_secret
    features {}
}