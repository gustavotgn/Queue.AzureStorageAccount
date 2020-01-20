//Step 1
//Package used to communicate with queue in Azure Storage Account

//To use, Add to Configuration file:

{
	"Values": {
		"AzureWebJobsStorage": "QueueConnectionString",
		"FUNCTIONS_WORKER_RUNTIME": "dotnet"
	
}

//Replace 'QueueConnectionString' with the Azure Storage Account connection string

//Step 2

//Add dependency injection to the Startup class:

        public IConfiguration Configuration { get; }

        private IHostingEnvironment _env;

        public void ConfigureServices(IServiceCollection services)
        {
		services.AddSingleton<IConfiguration>(Configuration);
		services.AddTransient<IQueueRepository, QueueRepository>();
	}


//ps: In the next version, a version will be added to save an item in the queue to a LocalHost file
