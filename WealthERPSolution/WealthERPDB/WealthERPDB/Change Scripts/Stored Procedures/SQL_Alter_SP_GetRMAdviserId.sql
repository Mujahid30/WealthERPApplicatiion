-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE SP_GetRMAdviserId


@AR_RMId INT

AS


SELECT 
	A_AdviserId 
FROM 
	AdviserRM
WHERE AR_RMId=@AR_RMId 