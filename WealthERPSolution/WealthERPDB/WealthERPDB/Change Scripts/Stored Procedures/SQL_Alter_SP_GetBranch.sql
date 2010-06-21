-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER procedure [dbo].[SP_GetBranch]
@AB_BranchId int
as
select * from AdviserBranch where AB_BranchId=@AB_BranchId
 