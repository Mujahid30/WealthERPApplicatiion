
ALTER PROCEDURE SP_GetProductMarketCapClassification
AS
BEGIN
	SET NOCOUNT ON;
	Select PMCC_MarketCapClassificationCode ,PMCC_CapClassification
	From ProductMarketCapClassification

END
 