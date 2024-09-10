# build the docker image
docker build --build-arg ENVIRONMENT=Production -t scheduler-api:latest -f Dockerfile .

# create a deployment
kubectl apply -f ./build/deployment.yaml

# create a service
kubectl apply -f ./build/service.yaml

# create a role
kubectl apply -f ./build/role.yaml

# create a role binding
kubectl apply -f ./build/rolebinding.yaml