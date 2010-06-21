
ALTER PROCEDURE [dbo].[SP_GetCashSavingsNetPosition]
@CCSNP_CashSavingsNPId INT
AS
BEGIN
	SELECT 
		* 
	FROM 
		CustomerCashSavingsNetPosition 
	WHERE 
		CCSNP_CashSavingsNPId = @CCSNP_CashSavingsNPId
END


 