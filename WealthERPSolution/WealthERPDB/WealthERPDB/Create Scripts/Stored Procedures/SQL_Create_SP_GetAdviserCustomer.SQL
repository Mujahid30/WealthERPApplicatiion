/****** Object:  StoredProcedure [dbo].[SP_GetAdviserCustomer]    Script Date: 06/24/2009 11:57:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAdviserCustomer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetAdviserCustomer]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAdviserCustomer]    Script Date: 06/24/2009 11:57:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAdviserCustomer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAdviserCustomer]
(
@A_AdviserId INT,
@CurrentPage INT = null,
@SortOrder VARCHAR(50) =N''C_FirstName ASC''
)

AS
IF(@CurrentPage IS NULL)
BEGIN

select C_CustomerId  FROM dbo.Customer 
INNER JOIN dbo.AdviserRM ON dbo.Customer.AR_RMId = dbo.AdviserRM.AR_RMId 
INNER JOIN dbo.Adviser ON dbo.AdviserRM.A_AdviserId = dbo.Adviser.A_AdviserId

WHERE dbo.AdviserRM.A_AdviserId=@A_AdviserId
END



 
ELSE IF(@CurrentPage IS NOT NULL)
BEGIN
DECLARE @intStartRow int; 
DECLARE @intEndRow int;
SET @intStartRow = (@CurrentPage -1) * 10 + 1;  
SET @intEndRow = @CurrentPage * 10;
WITH Entries AS
(
select 
	C_CustomerId,
	C_FirstName ,
	C_MiddleName,
	C_LastName,
	C_Email,
	ROW_NUMBER() over 
( 
ORDER BY
	CASE WHEN @SortOrder = ''C_FirstName DESC''
		THEN C_FirstName END DESC,
	CASE WHEN @SortOrder = ''C_FirstName ASC''
		THEN C_FirstName END ASC
) as RowNum 
FROM 
	dbo.Customer 
INNER JOIN dbo.AdviserRM  ON dbo.Customer.AR_RMId = dbo.AdviserRM.AR_RMId 
INNER JOIN dbo.Adviser ON dbo.AdviserRM.A_AdviserId = dbo.Adviser.A_AdviserId

WHERE dbo.AdviserRM.A_AdviserId=@A_AdviserId
)
Select * from Entries where RowNum BETWEEN @intStartRow AND @intEndRow

SELECT COUNT(*)   FROM dbo.Customer 
INNER JOIN dbo.AdviserRM ON dbo.Customer.AR_RMId = dbo.AdviserRM.AR_RMId 
INNER JOIN dbo.Adviser ON dbo.AdviserRM.A_AdviserId = dbo.Adviser.A_AdviserId

WHERE dbo.AdviserRM.A_AdviserId=@A_AdviserId
END





                     

' 
END
GO
