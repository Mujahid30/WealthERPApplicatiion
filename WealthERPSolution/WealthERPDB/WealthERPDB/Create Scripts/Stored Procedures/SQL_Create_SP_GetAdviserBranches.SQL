/****** Object:  StoredProcedure [dbo].[SP_GetAdviserBranches]    Script Date: 06/24/2009 11:57:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAdviserBranches]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetAdviserBranches]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAdviserBranches]    Script Date: 06/24/2009 11:57:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAdviserBranches]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================


CREATE procedure [dbo].[SP_GetAdviserBranches]
(
@CurrentPage INT = null,
@SortOrder VARCHAR(50) =''AB_BranchName ASC'',
@A_AdviserId INT
)
AS
BEGIN
SET NOCOUNT ON 
DECLARE @intStartRow int; 
DECLARE @intEndRow int;


-- Getting the records
IF(@CurrentPage is NULL)
BEGIN
Select * FROM AdviserBranch where A_AdviserId=@A_AdviserId	
END
ELSE IF(@CurrentPage IS NOT NULL)
BEGIN
SET @intStartRow = (@CurrentPage -1) * 10 + 1;  
SET @intEndRow = @CurrentPage * 10;
WITH Entries AS
(
select *,ROW_NUMBER() over ( ORDER BY
							CASE WHEN @SortOrder = ''AB_BranchName DESC''
							THEN AB_BranchName END DESC,
							CASE WHEN @SortOrder = ''AB_BranchName ASC''
							THEN AB_BranchName END ASC ) as RowNum 
 from AdviserBranch where A_AdviserId=@A_AdviserId
)
Select * from Entries where RowNum BETWEEN @intStartRow AND @intEndRow

-- Total Number of Records
SELECT COUNT(*)  from AdviserBranch where A_AdviserId=@A_AdviserId ;
END
SET NOCOUNT OFF;
END
' 
END
GO
