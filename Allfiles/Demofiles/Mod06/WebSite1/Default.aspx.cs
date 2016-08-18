using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Blob;

public partial class _Default : System.Web.UI.Page
{
    StorageCredentials credentials;
    CloudStorageAccount storageAccount;

    protected void Page_Load(object sender, EventArgs e)
    {
        string accountName = "YOUR_ACCOUNT_NAME";
        string accountKey = "YOUR_ACCOUNT_KEY";

        try
        {
            credentials = new StorageCredentials(accountName, accountKey);
            storageAccount = new CloudStorageAccount(credentials, useHttps: true);
            TextBox3.Text = "Connected to the storage account " + accountName;
        }
        catch
        {
        }
        finally
        {
        }
    }
    
    public class Customer : TableEntity
    {
        public Customer(string lastName, string firstName)
        {
            this.PartitionKey = lastName;
            this.RowKey = firstName;
        }

        public Customer() { }

        public string Email { get; set; }
        public string Phone { get; set; }
    }

    protected void btnCreateTable_Click(object sender, EventArgs e)
    {
        CloudTableClient client = storageAccount.CreateCloudTableClient();
        CloudTable table = client.GetTableReference("customers");
        table.CreateIfNotExists();
        TextBox3.Text += "\r\n" + "Created a new table customers";
    }

    protected void btnAddTableEntry_Click(object sender, EventArgs e)
    {
        // Create the table client.
        CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

        // Create the CloudTable object that represents the "customers" table.
        CloudTable table = tableClient.GetTableReference("customers");

        // Create a new customer entity.
        Customer customer1 = new Customer("Desforges", "Remi");
        customer1.Email = "remid@contoso.com";
        customer1.Phone = "425-555-1234";

        // Create the TableOperation that inserts the customer entity.
        TableOperation insertOperation = TableOperation.Insert(customer1);

        // Execute the insert operation.
        try
        {
            table.Execute(insertOperation);
        }
        catch
        {
        }
        TextBox3.Text += "\r\n" + "Added a single row to the customers table for Remi Desforges";

    }

    protected void btnAddTableBatch_Click(object sender, EventArgs e)
    {
        // Create the table client.
        CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

        // Create the CloudTable object that represents the "customers" table.
        CloudTable table = tableClient.GetTableReference("customers");

        // Create the batch operation.
        TableBatchOperation batchOperation = new TableBatchOperation();

        // Create a customer entity and add it to the table.
        Customer customer1 = new Customer("Gruber", "Karen");
        customer1.Email = "kareng@contoso.com";
        customer1.Phone = "425-555-2345";

        // Create another customer entity and add it to the table.
        Customer customer2 = new Customer("Gruber", "Paul");
        customer2.Email = "paulg@contoso.com";
        customer2.Phone = "425-555-3456";

        // Add both customer entities to the batch insert operation.
        batchOperation.Insert(customer1);
        batchOperation.Insert(customer2);

        // Execute the batch operation.
        try
        {
            table.ExecuteBatch(batchOperation);
        }
        catch
        {
        }
        TextBox3.Text += "\r\n" + "Added two rows in a batch to the customers table for Paul and Karen Gruber";

    }
    protected void btnRetrieveTableData_Click(object sender, EventArgs e)
    {
        // Create the table client.
        CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

        //Create the CloudTable object that represents the "customers" table.
        CloudTable table = tableClient.GetTableReference("customers");

        // Create the table query.
        TableQuery<Customer> rangeQuery = new TableQuery<Customer>().Where(
            TableQuery.CombineFilters(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Gruber"),
                TableOperators.And,
                TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.LessThan, "Z")));

        System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
        // Loop through the results, displaying information about the entity.
        foreach (Customer entity in table.ExecuteQuery(rangeQuery))
        {
            stringBuilder.Append(entity.PartitionKey).Append(" ").Append(entity.RowKey).Append(" ").Append(entity.Email).Append(" ").Append(entity.Phone).Append("\n");
            
        }
        TextBox1.Text = stringBuilder.ToString();

        TextBox3.Text = TextBox3.Text + "\r\n" + "Retrieved Paul and Karen Gruber data from the customers table";

    }

    protected void btnCreateBlobContainer_Click(object sender, EventArgs e)
    {
        CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
        CloudBlobContainer container = blobClient.GetContainerReference("container1");
        container.CreateIfNotExists();

        TextBox3.Text += "\r\n" + "Created a blob container container1";

    }
    protected void btnUploadToBlob_Click(object sender, EventArgs e)
    {
        // Create the blob client.
        CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

        // Retrieve reference to a previously created container.
        CloudBlobContainer container = blobClient.GetContainerReference("container1");

        // Retrieve reference to a blob named "myblob".
        CloudBlockBlob blockBlob = container.GetBlockBlobReference("blob1");

        // Create or overwrite the "myblob" blob with contents from a local file.
        using (var fileStream = System.IO.File.OpenRead(@"c:\Windows\win.ini"))
        {
            blockBlob.UploadFromStream(fileStream);
        }

        TextBox3.Text += "\r\n" + "Uploaded c:\\Windows\\win.ini to the blob container container1";

    }
    protected void btnListBlobContent_Click(object sender, EventArgs e)
    {
        // Create the blob client. 
        CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

        // Retrieve reference to a previously created container.
        CloudBlobContainer container = blobClient.GetContainerReference("container1");

        System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

        // Loop over items within the container and output the length and URI.
        foreach (IListBlobItem item in container.ListBlobs(null, false))
        {
            if (item.GetType() == typeof(CloudBlockBlob))
            {
                CloudBlockBlob blob = (CloudBlockBlob)item;
                stringBuilder.Append("Block blob of length ").Append(blob.Properties.Length).Append(" ").Append(blob.Uri).Append("\n");
            }
            else if (item.GetType() == typeof(CloudPageBlob))
            {
                CloudPageBlob pageBlob = (CloudPageBlob)item;
                stringBuilder.Append("Page blob of length ").Append(pageBlob.Properties.Length).Append(" ").Append(pageBlob.Uri).Append("\n");
            }
            else if (item.GetType() == typeof(CloudBlobDirectory))
            {
                CloudBlobDirectory directory = (CloudBlobDirectory)item;
                stringBuilder.Append("Directory ").Append(directory.Uri).Append("\n");
            }
        }
        TextBox2.Text = stringBuilder.ToString();

        TextBox3.Text += "\r\n" + "Retrieved list of blobs from the container container1";

    }

}