az ad sp create-for-rbac --skip-assignment

az acr login --name firstAksTestRegistry
docker tag 07api:api firstakstestregistry.azurecr.io/07-api:api
docker push firstakstestregistry.azurecr.io/07-api:api

az aks get-credentials --resource-group aks-test --name aksTestCluster