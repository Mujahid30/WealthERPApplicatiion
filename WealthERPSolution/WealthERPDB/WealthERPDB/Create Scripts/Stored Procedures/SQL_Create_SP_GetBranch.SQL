/****** Object:  StoredProcedure [dbo].[SP_GetBranch]    Script Date: 06/24/2009 11:58:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetBranch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetBranch]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetBranch]    Script Date: 06/24/2009 11:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetBranch]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE procedure [dbo].[SP_GetBranch]
@AB_BranchId int
as
select * from AdviserBranch where AB_BranchId=@AB_BranchId
' 
END
GO
