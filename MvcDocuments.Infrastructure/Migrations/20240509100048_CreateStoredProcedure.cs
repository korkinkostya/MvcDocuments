using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcDocuments.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR REPLACE FUNCTION public.calculate_total_price(
	xml_data xml)
    RETURNS numeric
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$
DECLARE
    total numeric := 0;
	line record;
BEGIN
    FOR line IN SELECT unnest(xpath('//Document/Rows/DocumentRow/Price/text()', xml_data))::text AS price
    LOOP
        total = total + CAST (line.price AS numeric);
    END LOOP;

    RETURN total;
END;
$BODY$;

ALTER FUNCTION public.calculate_total_price(xml)
    OWNER TO postgres;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.calculate_total_price");
        }
    }
}
