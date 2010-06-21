-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_DeleteEvent]
@EventSetupID BIGINT

AS

BEGIN
	SET NOCOUNT ON
	
	DELETE 
	FROM 
		dbo.AlertEventSetup
	WHERE
		AES_EventSetupID = @EventSetupID
		 
	SET NOCOUNT OFF
	
END 