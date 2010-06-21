-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetRMCustomerList]
(
@AR_RMId INT,
@CurrentPage INT =1,
@SortOrder varchar(50)=N'C_FirstName ASC',
@Flag VARCHAR(2) ='N'
)
AS
BEGIN
SET NOCOUNT ON ;
	DECLARE @intStartRow int; 
	DECLARE @intEndRow int;
	SET @intStartRow = (@CurrentPage -1) * 10 + 1;  
	SET @intEndRow = @CurrentPage * 10;
IF(@Flag='C')
				BEGIN
				select COUNT(*) from Customer where AR_RMId=@AR_RMId
				END
ELSE IF(@Flag='N')
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
						C2.C_FirstName +' '+ C2.C_MiddleName +' '+C2.C_LastName AS Parent,
						ROW_NUMBER() over ( ORDER BY
							CASE WHEN @SortOrder = 'C_FirstName DESC'
							THEN C1.C_FirstName + C1.C_MiddleName + C1.C_LastName END DESC,
							CASE WHEN @SortOrder = 'C_FirstName ASC'
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

--  