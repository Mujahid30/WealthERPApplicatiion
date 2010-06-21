




ALTER PROCEDURE [dbo].[SP_GetCommonTransUploadRejectsCount]
	-- Add the parameters for the stored procedure here
	@processId int,
	@assetType varchar(25)
	
AS
	SET NOCOUNT ON
	
	if @assetType = 'CA'
	begin
	
	SELECT COUNT(*) UploadCount from CustomerMFCAMSXtrnlTransactionStaging
	where CMCXTS_IsRejected = 1
	and ADUL_ProcessId = @processId
	
	end
	
	else if @assetType = 'KA'
	begin
	
	SELECT COUNT(*) UploadCount from CustomerMFKarvyXtrnlTransactionStaging
	where CIMFKXTS_IsRejected = 1
	and ADUL_ProcessId = @processId
	
	end
	
	else if @assetType = 'WP'
	begin
	
	SELECT COUNT(*) UploadCount from CustomerMFXtrnlTransactionStaging
	where CMFXTS_IsRejected = 1
	and ADUL_ProcessId = @processId	
	end
		
	SET NOCOUNT OFF




 