using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CreditCardNumbersValidator.Models;
using CreditCardNumbersValidator.Interfaces;

namespace CreditCardNumbersValidator.Controllers
{
    public class CreditCardsController : Controller
    {
        //private readonly CreditCardNumbersValidatorContext _context;
        private readonly ICreditCardNumberService _creditCardNumberService;

        public CreditCardsController(ICreditCardNumberService creditCardNumberService)
        {
            _creditCardNumberService = creditCardNumberService;
        }

        // GET: CreditCards
        public async Task<IActionResult> Index()
        {
           
            return View(await _creditCardNumberService.GetAll()); 
        }

        // GET: CreditCards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CreditCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CardNumber,CreditCardType")] CreditCard creditCard)
        {
            if (ModelState.IsValid)
            {
                var added = await _creditCardNumberService.AddValidCreditCardAsync(creditCard);

                TempData["AlertMessage"] = added;
                return RedirectToAction(nameof(Index));
            }
            return View(creditCard);
        }
    }
}
