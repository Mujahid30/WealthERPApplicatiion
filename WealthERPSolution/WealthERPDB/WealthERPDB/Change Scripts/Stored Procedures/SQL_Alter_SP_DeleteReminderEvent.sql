-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_DeleteReminderEvent]
@ParentEventSetupID BIGINT

AS

BEGIN
	SET NOCOUNT ON
	
	DELETE 
	FROM 
		dbo.AlertEventSetup
	WHERE
		AES_ParentEventSetupId = @ParentEventSetupID
		 
	SET NOCOUNT OFF
	
END 