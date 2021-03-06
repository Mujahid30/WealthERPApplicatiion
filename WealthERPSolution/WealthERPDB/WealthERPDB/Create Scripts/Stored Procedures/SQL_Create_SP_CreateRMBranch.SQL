/****** Object:  StoredProcedure [dbo].[SP_CreateRMBranch]    Script Date: 06/24/2009 11:13:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateRMBranch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreateRMBranch]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateRMBranch]    Script Date: 06/24/2009 11:13:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateRMBranch]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CreateRMBranch]

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
	
)' 
END
GO
