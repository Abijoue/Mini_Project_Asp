using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MiniProjet_Core.Model
{
    public class Subject
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdSubject { get; set; }

        [Required]
        public string NomSubject { get; set; }

        [Required]
        public string IdProf { get; set; }

        [Required]
        public int IdFiliere { get; set; }

        

        [ForeignKey("IdFiliere")]
        public Filiere Filiere { get; set; }

        [ForeignKey("IdProf")]
        public Utilisateur utilisateur { get; set; }
    }

}