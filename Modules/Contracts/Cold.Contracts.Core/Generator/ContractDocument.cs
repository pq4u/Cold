using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Cold.Contracts.Core.Generator;

public class ContractDocument : IDocument
{
    public ContractModel Model { get; }
    
    public ContractDocument(ContractModel model)
    {
        Model = model;
    }
    
    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public DocumentSettings GetSettings() => new DocumentSettings
    {
        ContentDirection = ContentDirection.LeftToRight
    };
    
    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.DefaultTextStyle(x => x.FontFamily("Calibri"));
                page.Margin(50);
            
                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);

                page.Footer().Text(x =>
                {
                    x.Span("Strona ");
                    x.CurrentPageNumber();
                    x.Span(" z ");
                    x.TotalPages();
                });
            });
    }

    void ComposeHeader(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item()
                    .Text($"Umowa dostawy płodów rolnych")
                    .AlignCenter()
                    .FontSize(20)
                    .SemiBold()
                    .FontColor(Colors.Black);

                column.Item().AlignCenter().Text(text =>
                {
                    text.Span($"z dnia: {Model.CreatedAt:yyyy-MM-dd}");
                });
                
                column.Item().AlignCenter().Text(text =>
                {
                    text.Span("zawarta w: Warszawa");
                });
                
                column.Item().AlignCenter().Text(text =>
                {
                    text.Span("pomiędzy: Adam Nowak");
                    text.Span($" a Januszex sp. z o.o.");
                });
                
                column.Item().Text("");
                column.Item().Text("");
            });
        });
        
        
    }

    void ComposeContent(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item()
                    .AlignCenter()
                    .Text($"§ 1 Przedmiot umowy")
                    .FontSize(16)
                    .FontColor(Colors.Black);

                column.Item().Text(text =>
                {
                    text.Span($"Przedmiotem umowy jest dostarczanie produktów rolnych: {string.Join(", ", Model.ProductsNames!)}");
                });
                
                column.Item().Text("");
                
                column.Item()
                    .AlignCenter()
                    .Text($"§ 2 Okres obowiązywania")
                    .FontSize(16)
                    .FontColor(Colors.Black);
                
                column.Item().Text(text =>
                {
                    text.Span($"Umowa zawarta jest na okres od {Model.StartDate:yyyy-MM-dd} do {Model.EndDate:yyyy-MM-dd}.");
                });
                
                column.Item().Text("");
                
                column.Item()
                    .AlignCenter()
                    .Text($"§ 7 Zaistnienie siły wyższej")
                    .FontSize(16)
                    .FontColor(Colors.Black);
                
                column.Item().Text(text =>
                {
                    text.Span($"Warunki umowy mogą ulec zmianie (za porozumieniem stron i wyłącznie w formie pisemnej)\nlub umowa może zostać rozwiązana ze skutkiem natychmiastowym w momencie zaistnienia\nsiły wyższej. Za siłę wyższą uważa się:\n● śmierć beneficjenta;\n● długoterminową niezdolność beneficjenta do wykonywania zawodu;\n● poważną klęskę żywiołową powodującą duże szkody w gospodarstwie rolnym;\n● zniszczenie w wyniku wypadku budynków inwentarskich w gospodarstwie rolnym;\n● chorobę epizootyczną lub chorobę roślin dotykającą odpowiednio cały inwentarz\nżywy lub uprawy należące do beneficjenta lub część tego inwentarza lub upraw;\n● wywłaszczenie całego lub dużej części gospodarstwa rolnego, jeśli takiego\nwywłaszczenia nie można było przewidzieć w dniu złożenia wniosku.");
                });
                
            });
        });
    }
}