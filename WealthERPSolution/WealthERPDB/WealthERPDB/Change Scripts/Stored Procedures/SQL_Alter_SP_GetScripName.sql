-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE SP_GetScripName

@PEM_ScripCode INT

AS

SELECT 
	*
FROM 
	dbo.ProductEquityMaster
WHERE 
	PEM_ScripCode=@PEM_ScripCode
 