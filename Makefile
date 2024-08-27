build:
	dotnet build

run:
	dotnet run --project=src/referral.csproj

test:
	dotnet test

init:
	dotnet clean
	dotnet build
	dotnet test
	dotnet run --project=src/referral.csproj
	