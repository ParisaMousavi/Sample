terraform{
    backend "remote" {
      organization = "parisa-training"
      workspaces {
          name = "sample-project"
      }
    }
}

provider "azurerm" {
  features {}
}