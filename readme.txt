
Package used to communicate with queue in Azure Storage Account

To use, Add to Configuration file:

{
	"Values": {
		"AzureWebJobsStorage": "QueueConnectionString",
		"FUNCTIONS_WORKER_RUNTIME": "dotnet"
	
}

Replace 'QueueConnectionString' with the Azure Storage Account connection string