
terraform {
  backend "remote" {
    hostname     = "app.terraform.io"
    organization = "parisa-training"
    workspaces {
      name = "sample-project"
    }
  }
}
