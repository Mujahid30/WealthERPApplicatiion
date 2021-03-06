/****** Object:  StoredProcedure [dbo].[SP_CreateRoleAssociation]    Script Date: 06/24/2009 11:14:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateRoleAssociation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreateRoleAssociation]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateRoleAssociation]    Script Date: 06/24/2009 11:14:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateRoleAssociation]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CreateRoleAssociation]

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
' 
END
GO
