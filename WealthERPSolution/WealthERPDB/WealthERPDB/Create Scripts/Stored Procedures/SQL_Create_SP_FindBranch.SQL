/****** Object:  StoredProcedure [dbo].[SP_FindBranch]    Script Date: 06/24/2009 11:15:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_FindBranch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_FindBranch]
GO
/****** Object:  StoredProcedure [dbo].[SP_FindBranch]    Script Date: 06/24/2009 11:15:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_FindBranch]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_FindBranch]
@AB_BranchName VARCHAR(25),
@A_AdviserId int


AS

SELECT AB_BranchId FROM dbo.AdviserBranch WHERE AB_BranchName LIKE  @AB_BranchName+ ''%''AND A_AdviserId=@A_AdviserId
' 
END
GO
