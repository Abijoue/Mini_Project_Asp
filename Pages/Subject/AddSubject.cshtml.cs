using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniProjet_Core.Data;
using MiniProjet_Core.Model;

namespace MyApp.Namespace
{
     [Authorize(Roles = "Admin")]
    public class AddSubjectModel : PageModel
    {
       private readonly ApplicationDbContext _db;
      [BindProperty]
      public Subject subject {get;set;}
      public IList<Filiere> filieres {get ; private set ;}
      public IList<Utilisateur> users {get ; private set ;}


      public AddSubjectModel(ApplicationDbContext db ){
            _db = db;
        }
        public void OnGet()
        {
            filieres = _db.Filieres.ToList();
            users = _db.Utilisateurs.ToList();
        }
        public IActionResult OnPost(){

            if(ModelState.IsValid){
                var DB_Sub = _db.Subjects.Find(subject.IdSubject);
                if(DB_Sub != null){
                    return Page();    
                }
                _db.Subjects.Add(subject);
                _db.SaveChanges();
                return RedirectToPage("/Subject/SubjectsList");
            }else{
                return Page();
            }
        }
    }
}
