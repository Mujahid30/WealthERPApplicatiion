
ALTER PROCEDURE SP_GetProductSectorClassification 
AS

BEGIN

 SET NOCOUNT ON;
	
 Select PSC_SectorId,PSC_SectorName
 FROM ProductSectorClassification 
 
END
 