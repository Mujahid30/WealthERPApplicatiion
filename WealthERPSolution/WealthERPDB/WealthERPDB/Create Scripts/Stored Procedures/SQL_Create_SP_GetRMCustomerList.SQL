/****** Object:  StoredProcedure [dbo].[SP_GetRMCustomerList]    Script Date: 06/24/2009 12:04:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetRMCustomerList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetRMCustomerList]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetRMCustomerList]    Script Date: 06/24/2009 12:04:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetRMCustomerList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetRMCustomerList]
(
@AR_RMId INT,
@CurrentPage INT =1,
@SortOrder varchar(50)=N''C_FirstName ASC'',
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
				select COUNT(*) from Customer where AR_RMId=@AR_RMId
				END
ELSE IF(@Flag=''N'')
				BEGIN
				WITH Entries AS
				(
				 SELECT C1.C_CustomerId,
						C1.C_FirstName,
						C1.C_MiddleName,
						C1.C_LastName,
						C1.C_ResISDCode,
						C1.C_ResSTDCode,
						C1.C_ResPhoneNum,
						C1.U_UMId,
						C1.AR_RMId,
						C1.C_CustCode,
						C1.C_CompanyName,
						C1.C_Email,
						CA.C_CustomerId as ParentId,
						C2.C_FirstName +'' ''+ C2.C_MiddleName +'' ''+C2.C_LastName AS Parent,
						ROW_NUMBER() over ( ORDER BY
							CASE WHEN @SortOrder = ''C_FirstName DESC''
							THEN C1.C_FirstName + C1.C_MiddleName + C1.C_LastName END DESC,
							CASE WHEN @SortOrder = ''C_FirstName ASC''
							THEN  C1.C_FirstName + C1.C_MiddleName + C1.C_LastName END ASC  ) as RowNum
				 FROM Customer C1 LEFT OUTER JOIN CustomerAssociates CA ON C1.C_CustomerId=CA.C_AssociateCustomerId
								  LEFT OUTER JOIN Customer C2 on CA.C_CustomerId=C2.C_CustomerId
				 WHERE C1.AR_RMId=@AR_RMId 
				)
				 SELECT * FROM Entries where RowNum BETWEEN @intStartRow AND @intEndRow
				 
				 PRINT @SortOrder;
				END
SET NOCOUNT OFF;
END

-- ' 
END
GO
