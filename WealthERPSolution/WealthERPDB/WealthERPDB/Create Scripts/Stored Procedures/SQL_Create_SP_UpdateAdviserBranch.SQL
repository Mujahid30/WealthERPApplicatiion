/****** Object:  StoredProcedure [dbo].[SP_UpdateAdviserBranch]    Script Date: 06/24/2009 12:05:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateAdviserBranch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_UpdateAdviserBranch]
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateAdviserBranch]    Script Date: 06/24/2009 12:05:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_UpdateAdviserBranch]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateAdviserBranch]
@AB_BranchId bigint,
@AB_BranchCode varchar(10),
@AB_BranchName varchar(25),
@AB_AddressLine1 varchar(25),
@AB_AddressLine2 varchar(25),
@AB_AddressLine3 varchar(25),
@AB_Pincode numeric(6,0),
@AB_City varchar(25),
@AB_State varchar(25),
@AB_Country varchar(25),
@AB_Email varchar(50),
@AB_Phone1ISD numeric(3,0),
@AB_Phone1STD numeric(3,0),
@AB_Phone1 numeric(8,0),
@AB_Phone2ISD numeric(3,0),
@AB_Phone2STD numeric(3,0),
@AB_Phone2 numeric(8,0),
@AB_FaxISD numeric(3,0),
@AB_FaxSTD numeric(3,0),
@AB_Fax numeric(8,0)

AS

update AdviserBranch set 
AB_BranchCode=@AB_BranchCode,

AB_BranchName=@AB_BranchName,
AB_AddressLine1=@AB_AddressLine1,
AB_AddressLine2=@AB_AddressLine2,
AB_AddressLine3=@AB_AddressLine3,
AB_City=@AB_City,
AB_PinCode=@AB_PinCode,
AB_State=@AB_State,
AB_Country=@AB_Country,
AB_Email=@AB_Email,
AB_Phone1ISD=@AB_Phone1ISD,
AB_Phone2ISD=@AB_Phone2ISD,
AB_Phone1STD=@AB_Phone1STD,
AB_Phone1=@AB_Phone1,
AB_Phone2STD=@AB_Phone2STD,
AB_Phone2=@AB_Phone2,
AB_FaxISD=@AB_FaxISD,
AB_Fax=@AB_Fax,
AB_FaxSTD=@AB_FaxSTD

where AB_BranchId=@AB_BranchId

' 
END
GO
