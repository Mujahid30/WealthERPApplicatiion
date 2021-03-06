/****** Object:  StoredProcedure [dbo].[SP_GetAdviserStaff]    Script Date: 06/24/2009 11:57:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAdviserStaff]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetAdviserStaff]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAdviserStaff]    Script Date: 06/24/2009 11:57:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAdviserStaff]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE Procedure [dbo].[SP_GetAdviserStaff]
(
@A_AdviserId INT,
@CurrentPage INT = null,
@SortOrder VARCHAR(50) =''AR_FirstName ASC''

)
AS
IF(@CurrentPage IS NULL)
BEGIN
select * from dbo.AdviserRM where A_AdviserId=@A_AdviserId	
END

ELSE IF(@CurrentPage IS NOT NULL)
BEGIN
DECLARE @intStartRow int; 
DECLARE @intEndRow int;
SET @intStartRow = (@CurrentPage -1) * 10 + 1;  
SET @intEndRow = @CurrentPage * 10;
WITH Entries AS
(
select * , ROW_NUMBER() over ( ORDER BY
							CASE WHEN @SortOrder = ''AR_FirstName DESC''
							THEN AR_FirstName END DESC,
							CASE WHEN @SortOrder = ''AR_FirstName ASC''
							THEN AR_FirstName END ASC ) as RowNum 
 from  dbo.AdviserRM where A_AdviserId=@A_AdviserId	
)
Select * from Entries where RowNum BETWEEN @intStartRow AND @intEndRow

SELECT COUNT(*)   from dbo.AdviserRM where A_AdviserId=@A_AdviserId	
END




' 
END
GO
