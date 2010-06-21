 -- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE SP_CheckRMBranchAssociation

@AR_RMId INT,
@AB_BranchId INT

AS

SELECT COUNT(*) FROM dbo.AdviserRMBranch WHERE AR_RMId=@AR_RMId AND AB_BranchId=@AB_BranchId

--EXEC SP_CheckRMBranchAssociation 1053,1022
--	@AR_RMId = 0, -- INT
--	@AB_BranchId = 0 -- INT
