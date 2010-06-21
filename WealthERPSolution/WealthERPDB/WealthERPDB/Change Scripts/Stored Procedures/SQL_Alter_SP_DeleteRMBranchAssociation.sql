-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE SP_DeleteRMBranchAssociation

@AR_RMId INT,
@AB_BranchId INT

AS

DELETE FROM dbo.AdviserRMBranch WHERE AB_BranchId=@AB_BranchId AND AR_RMId=@AR_RMId 