-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE SP_SelectEQTransactionByScrip

@PEM_ScripCode INT

AS

SELECT
	 * 
FROM 
	dbo.CustomerEquityTransaction 
		
WHERE PEM_ScripCode=@PEM_ScripCode
 