using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

using WonkaBlazorWebApp.Client.Monaco;

namespace WonkaBlazorWebApp.Client.Pages
{
    public class StoreMarkupIndexModel : ComponentBase
    {
        protected bool   requestDone;
        protected string rulesFile;
        protected string rulesMarkup;
        protected string markupKey;
        protected string Output = "";

        protected EditorModel editorModel;

        public int SelectedCodeSample { get; protected set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected HttpClient Client { get; set; }

        protected async Task GetStatus()
        {
            if (String.IsNullOrEmpty(markupKey))
            {
                markupKey = await Client.GetJsonAsync<string>("api/markup?MarkupStorageUrl=LastMarkupGenKey");
                // markupKey = Client.GetJsonAsync<string>("api/markup?MarkupStorageUrl=LastMarkupGenKey").Result;
            }
        }

        protected async Task LoadFromFile()
        {
            if (String.IsNullOrEmpty(rulesFile))
            {
                rulesMarkup = System.IO.File.ReadAllText(rulesFile);
            }
        }

        protected override Task OnInitAsync()
        {
            rulesMarkup =
    @"<?xml version=""1.0""?>
<RuleTree xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">

<if description=""Start a new sales transaction"">
  <criteria op=""AND"">
     <eval id=""pop1"">(N.NewSalesTransSeq) POPULATED</eval>
     <eval id=""asn1"">(N.NewSalesTransSeq) ASSIGN ('1')</eval>
  </criteria>

  <if description=""Checking Input Values"">
       <criteria op=""AND"">
           <eval id=""pop2"">(N.NewSaleEAN) POPULATED</eval>
           <eval id=""asn2"">(N.NewSalesTransSeq) ASSIGN ('2')</eval>
       </criteria>

       <validate err=""severe"">
           <criteria op=""AND"">
               <eval id=""cmp2"">(N.NewSalePrice) GT (0.00)</eval>
               <eval id=""asn3"">(N.NewSalesTransSeq) ASSIGN ('3')</eval>
           </criteria>

           <failure_message>ERROR!  Required inputs for VAT calculation have not been provided.</failure_message>
           <success_message/>
       </validate>

      <validate err=""severe"">
           <criteria op=""AND"">
               <eval id=""cmp2"">(N.NewSalePrice) LT (1000000.00)</eval>
               <eval id=""asn4"">(N.NewSalesTransSeq) ASSIGN ('4')</eval>
           </criteria>

           <failure_message>ERROR!  Required inputs for VAT calculation exceed maximum allowed value.</failure_message>
           <success_message/>
       </validate>

   </if>
</if>
</RuleTree>
";

            rulesFile = "";
            markupKey = "dummyKey";

            SelectedCodeSample = 0;

            editorModel = new EditorModel
            {
                Language = "xml",
                Script   = rulesMarkup
            };

            return base.OnInitAsync();
        }

        public void Run()
        {
            // Compiler.WhenReady(RunInternal);
        }

        public async Task RunInternal()
        {
            Output = "";

            editorModel = await Monaco.Interop.EditorGetAsync(JSRuntime, editorModel);

            StateHasChanged();
        }

        protected async Task Submit()
        {
            requestDone = false;
            markupKey   = "";
            rulesFile   = "";

            // var response = httpClient.GetAsync("http://localhost:56653/api/markup?markupid=http://localwonkacache/7f5e9416-249a-4151-9db8-da2888ac0ab2").Result;
            // markupKey = Http.SendJsonAsync<string>(System.Net.Http.HttpMethod.Post, "api/markup", rulesMarkup).Result;

            editorModel = await Monaco.Interop.EditorGetAsync(JSRuntime, editorModel);

            rulesMarkup = editorModel.Script;

            await Client.SendJsonAsync<string>(System.Net.Http.HttpMethod.Post, "api/markup", rulesMarkup);

            // markupKey = await Http.GetJsonAsync<string>("api/markup?MarkupStorageUrl=LastMarkupGenKey");

            await GetStatus();
        }

    }

}
