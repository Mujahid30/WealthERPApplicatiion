-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetFixedIncomeNetPosition]
@CFINP_FINPId INT
AS
BEGIN
	SELECT 
		* 
	FROM 
		CustomerFixedIncomeNetPosition 
	WHERE 
		CFINP_FINPId = @CFINP_FINPId
END 