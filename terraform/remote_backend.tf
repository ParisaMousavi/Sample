# terraform{
#     backend "azurerm" {
#         resource_group_name = "sample-terraform-rg"
#         storage_account_name = "sampleterraform"
#         container_name = "terrform"
#         key = "development.terraform.tfstate"
#     }
# }

terraform {
  backend "remote" {
    hostname     = "app.terraform.io"
    organization = "parisa-training"
    workspaces {
      name = "sample-project"
    }
  }
}
