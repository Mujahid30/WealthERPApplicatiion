ALTER PROCEDURE [dbo].[SP_DeleteAlertNotification]
@NotificationID BIGINT

AS

BEGIN

	SET NOCOUNT ON
	
	DELETE 
	FROM
		dbo.AlertEventNotification
	WHERE
		AEN_EventQueueID = @NotificationID 
	
	SET NOCOUNT OFF
	
END 