/****** Object:  StoredProcedure [dbo].[SP_GetAdviserCustomerList]    Script Date: 06/24/2009 11:57:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAdviserCustomerList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetAdviserCustomerList]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAdviserCustomerList]    Script Date: 06/24/2009 11:57:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAdviserCustomerList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAdviserCustomerList]
(
@A_AdviserId INT,
@CurrentPage INT = null,
@SortOrder VARCHAR(50) =N''C_FirstName ASC'',
@Flag VARCHAR(2) =''N''
)

AS
BEGIN
SET NOCOUNT ON ;
	DECLARE @intStartRow int; 
	DECLARE @intEndRow int;
	SET @intStartRow = (@CurrentPage -1) * 10 + 1;  
	SET @intEndRow = @CurrentPage * 10;
	
IF(@Flag=''C'')
				BEGIN
				select COUNT(*) from Customer 
					INNER JOIN dbo.AdviserRM ON dbo.Customer.AR_RMId = dbo.AdviserRM.AR_RMId 
					INNER JOIN dbo.Adviser ON dbo.AdviserRM.A_AdviserId = dbo.Adviser.A_AdviserId
					WHERE dbo.AdviserRM.A_AdviserId=@A_AdviserId
				END
ELSE IF(@Flag=''N'')
				BEGIN
				WITH Entries AS
				(
				 SELECT 
					C_CustomerId,
					C_FirstName,
					C_MiddleName,
					C_LastName,
					C_ResISDCode,
					C_ResSTDCode,
					C_ResPhoneNum,
					U_UMId,
					C_CustCode,
					C_CompanyName,
				    C_Email,
				 ROW_NUMBER() over ( ORDER BY
							CASE WHEN @SortOrder = ''C_FirstName DESC''
							THEN C_FirstName + C_MiddleName + C_LastName END DESC,
							CASE WHEN @SortOrder = ''C_FirstName ASC''
							THEN  C_FirstName + C_MiddleName + C_LastName END ASC  ) as RowNum
					from Customer 
					INNER JOIN dbo.AdviserRM ON dbo.Customer.AR_RMId = dbo.AdviserRM.AR_RMId 
					INNER JOIN dbo.Adviser ON dbo.AdviserRM.A_AdviserId = dbo.Adviser.A_AdviserId
					WHERE dbo.AdviserRM.A_AdviserId=@A_AdviserId
				 )
				 SELECT * FROM Entries where RowNum BETWEEN @intStartRow AND @intEndRow
				 
				 PRINT @SortOrder;
				END
SET NOCOUNT OFF;
END


' 
END
GO
