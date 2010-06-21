-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE SP_DeleteRMDetails

@AR_RMId int


AS
DELETE FROM dbo.AdviserRM WHERE AR_RMId=@AR_RMId
 