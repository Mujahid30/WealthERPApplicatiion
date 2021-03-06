/****** Object:  UserDefinedFunction [dbo].[fnProcessAlertDataCondition]    Script Date: 06/24/2009 15:30:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnProcessAlertDataCondition]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnProcessAlertDataCondition]
GO
/****** Object:  UserDefinedFunction [dbo].[fnProcessAlertDataCondition]    Script Date: 06/24/2009 15:30:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnProcessAlertDataCondition]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
Create FUNCTION [dbo].[fnProcessAlertDataCondition](@targetId int, @schemeId int,@eventId int,@currentValue numeric(18,3))
RETURNS bit
AS
BEGIN

-- Declaration of Varoiables
declare @IsConditionMatched bit, 
@operator as varchar(4),@presetValue as numeric(18,3)

-- SET default Values
SET @IsConditionMatched = 0 -- default value is false...means condition does not match

-- Get the Operator and the Preset Value
SELECT @operator = ADCS_Condition, @presetValue = ADCS_PresetValue
FROM AlertDataConditionSetup
Where ((ADCS_UserId = @targetId) AND  (ADCS_SchemeId = @schemeId) AND (AEL_EventId = @eventId))

-- Processing

IF(@operator = ''>'')
BEGIN
	IF(@currentValue) > (@presetValue)
	BEGIN
		SET @IsConditionMatched = 1	
		RETURN @IsConditionMatched
	END
END

IF(@operator = ''<'')
BEGIN
	IF(@currentValue) < (@presetValue)
	BEGIN
		SET @IsConditionMatched = 1	
		RETURN @IsConditionMatched
	END
END

IF(@operator = ''='')
BEGIN
	IF(@currentValue) = (@presetValue)
	BEGIN
		SET @IsConditionMatched = 1	
		RETURN @IsConditionMatched
	END
END

IF(@operator = ''>='')
BEGIN
	IF(@currentValue) >= (@presetValue)
	BEGIN
		SET @IsConditionMatched = 1	
		RETURN @IsConditionMatched
	END
END
IF(@operator = ''<='')
BEGIN
	IF(@currentValue) <= (@presetValue)
	BEGIN
		SET @IsConditionMatched = 1	
		RETURN @IsConditionMatched
	END
END
IF(@operator = ''<>'')
BEGIN
	IF(@currentValue) <> (@presetValue)
	BEGIN
		SET @IsConditionMatched = 1	
		RETURN @IsConditionMatched
	END
END

RETURN @IsConditionMatched
END 


--select dbo.fnProcessAlertDataCondition(1011,1001,11,500000.000)' 
END

GO
