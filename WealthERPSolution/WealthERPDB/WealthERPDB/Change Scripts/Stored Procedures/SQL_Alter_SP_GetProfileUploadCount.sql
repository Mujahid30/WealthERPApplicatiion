-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetProfileUploadCount]
 -- Add the parameters for the stored procedure here
 @processId int,
 @assetType varchar(25)
 
AS
 SET NOCOUNT ON
 
 if @assetType = 'CA'
 begin
 
 SELECT COUNT(*) UploadCount from CustomerMFCAMSXtrnlProfileStaging
 where CMGCXPS_IsRejected = 0
 and ADUL_ProcessId = @processId
 
 end
 
 else if @assetType = 'KA'
 begin
 
 SELECT COUNT(*) UploadCount from CustomerMFKarvyXtrnlProfileStaging
 where CMFKXPS_IsRejected = 0
 and ADUL_ProcessId = @processId
 
 end
 
 else if @assetType = 'WP'
 begin
 
 SELECT COUNT(*) UploadCount from CustomerMFXtrnlProfileStaging
 where CMFXPS_IsRejected = 0
 and ADUL_ProcessId = @processId 
 end
 