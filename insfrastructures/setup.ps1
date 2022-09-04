$grp = "DEV-CONTAINER-APP-RG"
$loc = "westus"
$environment = "cne-dpr"
$STORAGE_ACCOUNT = "azstorageaccdapr"

# sometime azure need register some extensions that help to create container apps, so we need install these extension in advance
az provider register -n Microsoft.App --wait

# creating resource group
az group create --name $grp `
                --location $loc

# creating storage account
az storage account create --name $STORAGE_ACCOUNT `
                --resource-group $grp `
                --location $loc `
                --sku Standard_RAGRS `
                --kind StorageV2

# creating environment
az containerapp env create --name $environment `
                           --resource-group $grp `
                           --internal-only false `
                           --location $loc

# setting dapr state store
az containerapp env dapr-component set `
--name $environment --resource-group $grp `
--dapr-component-name statestore `
--yaml '.\dapr\components\eshop-statestore.yaml'

az containerapp env dapr-component set `
--name $environment --resource-group $grp `
--dapr-component-name secretstore `
--yaml '.\dapr\components\eshop-secretstore.yaml'

az containerapp env dapr-component list --resource-group $grp --name $environment --output json

# rebuild images
docker build -t toanduong08042020/dapr.eshop.catalog:latest -f 'backends\dapr.eshop.catalog\Dockerfile' .
docker push toanduong08042020/dapr.eshop.catalog:latest

# creating catalog service
az containerapp create `
  --name catalog-api `
  --resource-group $grp `
  --environment $environment `
  --image toanduong08042020/dapr.eshop.catalog:latest `
  --target-port 80 `
  --ingress 'internal' `
  --min-replicas 1 `
  --max-replicas 5 `
  --enable-dapr `
  --env-vars ASPNETCORE_ENVIRONMENT="Development" `
  --dapr-app-port 80 `
  --dapr-app-id catalog-api
