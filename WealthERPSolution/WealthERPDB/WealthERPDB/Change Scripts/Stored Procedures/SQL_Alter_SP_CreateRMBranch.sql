-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreateRMBranch]

@AR_RMId	int,
@AB_BranchId	int,
@ARB_CreatedBy	int,
@ARB_ModifiedBy	int

	

as

insert into AdviserRMBranch
(
AR_RMId	,
AB_BranchId	,
ARB_CreatedBy,
ARB_CreatedOn,
ARB_ModifiedBy,
ARB_ModifiedOn
	


)
 values
(
@AR_RMId	,
@AB_BranchId	,
@ARB_CreatedBy,
current_timestamp,
@ARB_ModifiedBy,
current_timestamp
	
) 