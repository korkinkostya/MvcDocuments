using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;
using System.Xml.Serialization;

namespace MvcDocuments.Domain.Entities;

public class Document
{
    [Key] public int Id { get; set; }


    [Display(Name = "Номер документа")] public string Number { get; set; }

    [Display(Name = "Дата")]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }


    public Document()
    {
        Rows = new List<DocumentRow>();
    }


    public List<DocumentRow> Rows { get; set; }


    [Display(Name = "Сумма всех цен")]
    [NotMapped]
    [Column(TypeName = "decimal(18, 2)")]
    [DisplayFormat(DataFormatString = "{0:G29}", ApplyFormatInEditMode = true)]
    public decimal? TotalPrice { get; set; }


    public string ToXml()
    {
        XmlSerializer xsSubmit = new XmlSerializer(typeof(Document));

        var xml = string.Empty;


        XmlWriterSettings settings = new XmlWriterSettings { Indent = true };
        using (var sww = new StringWriter())
        {
            using (XmlWriter writer = XmlWriter.Create(sww, settings))
            {
                xsSubmit.Serialize(writer, this);
                xml = sww.ToString();
            }
        }

        return xml;
    }
}