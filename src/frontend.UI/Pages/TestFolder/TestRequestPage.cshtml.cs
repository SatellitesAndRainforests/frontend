using frontend.Application.Interfaces;
using frontend.Application.Services;
using frontend.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace frontend.UI.Pages.TestFolder
{
    public class TestRequestPageModel : PageModel
    {

        readonly ITestService _testService;

        public TestModel? TestModel { get; set; }

        public TestRequestPageModel(ITestService testService)
        {
            _testService = testService;
        }

        public async Task<IActionResult> OnGet()
        {
            TestModel = await _testService.MakeApiCall();

            return Page();
        }

    }
}
