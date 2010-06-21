-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreateRoleAssociation]

@U_UserId	int,
@UR_RoleId	INT,
@URA_CreatedBy INT,
@URA_ModifiedBy INT	
AS
INSERT INTO UserRoleAssociation
(U_UserId,
UR_RoleId,
URA_CreatedBy,
URA_CreatedOn,
URA_ModifiedBy,
URA_ModifiedOn
		
)
VALUES(@U_UserId,@UR_RoleId, @URA_CreatedBy, CURRENT_TIMESTAMP, @URA_ModifiedBy, CURRENT_TIMESTAMP)
 