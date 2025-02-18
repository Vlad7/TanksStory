; Èìÿ èñïîëíÿåìîãî ìîäóëÿ
#define   ExeName    "TanksStory.exe"
                                          
; Êàòàëîã èñïîëíÿåìîãî ìîäóëÿ
#define   AppDirectory "..\TanksStory\bin\Release\"

; Ñàôò ôèðìû ðàçðàáîò÷èêà
#define   URL        "https://github.com/Vlad7/TanksStory"

; Èìÿ èêîíêè
#define   IconName   "..\TanksStory\Icon.ico"

; Èìÿ ïðèëîæåíèÿ
#define   Name       GetStringFileInfo(AppDirectory+ExeName, "ProductName")

; Ôèðìà-ðàçðàáîò÷èê
#define   Publisher  GetStringFileInfo(AppDirectory+ExeName, "CompanyName")

; Âåðñèÿ ïðèëîæåíèÿ
#define   Version    GetStringFileInfo(AppDirectory+ExeName, "FileVersion")


;------------------------------------------------------------------------------
;   Ïàðàìåòðû óñòàíîâêè
;------------------------------------------------------------------------------
[Setup]

; Óíèêàëüíûé èäåíòèôèêàòîð ïðèëîæåíèÿ, 
;ñãåíåðèðîâàííûé ÷åðåç Tools -> Generate GUID
AppId={{4D5CE2A1-9570-456B-82A7-39990D480B27}}


; Ïðî÷àÿ èíôîðìàöèÿ, îòîáðàæàåìàÿ ïðè óñòàíîâêå
AppName={#ExeName}
AppVersion={#Version}
AppPublisher={#Publisher}
AppPublisherURL={#URL}
AppSupportURL={#URL}
AppUpdatesURL={#URL}

; Ïóòü óñòàíîâêè ïî-óìîë÷àíèþ
DefaultDirName={pf}\{#Name}
; Èìÿ ãðóïïû â ìåíþ "Ïóñê"
DefaultGroupName={#Name}

; Êàòàëîã, êóäà áóäåò çàïèñàí ñîáðàííûé setup è èìÿ èñïîëíÿåìîãî ôàéëà
OutputDir=Setup
OutputBaseFileName=TanksStoryInstaller

; Ôàéë èêîíêè
SetupIconFile={#IconName}

; Ïàðàìåòðû ñæàòèÿ
Compression=lzma
SolidCompression=yes


;------------------------------------------------------------------------------
;   Óñòàíàâëèâàåì ÿçûêè äëÿ ïðîöåññà óñòàíîâêè
;------------------------------------------------------------------------------
[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"; 
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl"; 


;------------------------------------------------------------------------------
;   Îïöèîíàëüíî - íåêîòîðûå çàäà÷è, êîòîðûå íàäî âûïîëíèòü ïðè óñòàíîâêå
;------------------------------------------------------------------------------
[Tasks]
; Ñîçäàíèå èêîíêè íà ðàáî÷åì ñòîëå
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked


;------------------------------------------------------------------------------
;   Ôàéëû, êîòîðûå íàäî âêëþ÷èòü â ïàêåò óñòàíîâùèêà
;------------------------------------------------------------------------------
[Files]

; Èñïîëíÿåìûé ôàéë
Source: "..\TanksStory\bin\Release\TanksStory.exe"; DestDir: "{app}"; Flags: ignoreversion

; Ïðèëàãàþùèåñÿ ðåñóðñû
Source: "..\TanksStory\bin\Release\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

;------------------------------------------------------------------------------
;   Óêàçûâàåì óñòàíîâùèêó, ãäå îí äîëæåí âçÿòü èêîíêè
;------------------------------------------------------------------------------ 
[Icons]

Name: "{group}\{#Name}"; Filename: "{app}\{#ExeName}"

Name: "{commondesktop}\{#Name}"; Filename: "{app}\{#ExeName}"; Tasks: desktopicon




[Code]
function GetUninstallString: string;
var
  sUnInstPath: string;
  sUnInstallString: String;
begin
  Result := '';
  sUnInstPath := ExpandConstant('Software\Microsoft\Windows\CurrentVersion\Uninstall\{{667DDB08-76CB-4DB6-B35D-D4B48F4AD8CB}_is1'); //Your App GUID/ID
  sUnInstallString := '';
  if not RegQueryStringValue(HKLM, sUnInstPath, 'UninstallString', sUnInstallString) then
    RegQueryStringValue(HKCU, sUnInstPath, 'UninstallString', sUnInstallString);
  Result := sUnInstallString;
end;

function IsUpgrade: Boolean;
begin
  Result := (GetUninstallString() <> '');
end;

function IsDotNetDetected(version: string; release: cardinal): boolean;

var 
    reg_key: string; 			// Ïðîñìàòðèâàåìûé ïîäðàçäåë ñèñòåìíîãî ðååñòðà
    success: boolean; 			// Ôëàã íàëè÷èÿ çàïðàøèâàåìîé âåðñèè .NET
    release45: cardinal; 		// Íîìåð ðåëèçà äëÿ âåðñèè 4.5.x
    key_value: cardinal;	                // Ïðî÷èòàííîå èç ðååñòðà çíà÷åíèå êëþ÷à
    sub_key: string;

begin

    success := false;
    reg_key := 'SOFTWARE\Microsoft\NET Framework Setup\NDP\';
    
    // Âåðñèÿ 3.0

    if Pos('v3.0', version) = 1 then
      begin
          sub_key := 'v3.0';
          reg_key := reg_key + sub_key;
          success := RegQueryDWordValue(HKLM, reg_key, 'InstallSuccess', key_value);
          success := success and (key_value = 1);
      end;

    // Âåðñèÿ 3.5

    if Pos('v3.5', version) = 1 then
      begin
          sub_key := 'v3.5';
          reg_key := reg_key + sub_key;
          success := RegQueryDWordValue(HKLM, reg_key, 'Install', key_value);
          success := success and (key_value = 1);
      end;

     // Âåðñèÿ 4.0 êëèåíòñêèé ïðîôèëü

     if Pos('v4.0 Client Profile', version) = 1 then
      begin
          sub_key := 'v4\Client';
          reg_key := reg_key + sub_key;
          success := RegQueryDWordValue(HKLM, reg_key, 'Install', key_value);
          success := success and (key_value = 1);
      end;

     // Âåðñèÿ 4.0 ðàñøèðåííûé ïðîôèëü

     if Pos('v4.0 Full Profile', version) = 1 then
      begin
          sub_key := 'v4\Full';
          reg_key := reg_key + sub_key;
          success := RegQueryDWordValue(HKLM, reg_key, 'Install', key_value);
          success := success and (key_value = 1);
      end;

     // Âåðñèÿ 4.5

     if Pos('v4.5', version) = 1 then
      begin
          sub_key := 'v4\Full';
          reg_key := reg_key + sub_key;
          success := RegQueryDWordValue(HKLM, reg_key, 'Release', release45);
          success := success and (release45 >= release);
      end;
        
    result := success;

end;

//-----------------------------------------------------------------------------
//  Ôóíêöèÿ-îáåðòêà äëÿ äåòåêòèðîâàíèÿ êîíêðåòíîé íóæíîé íàì âåðñèè
//-----------------------------------------------------------------------------
function IsRequiredDotNetDetected(): boolean;
begin
    result := IsDotNetDetected('v4.0 Full Profile', 0);
end;



//-----------------------------------------------------------------------------
//    Callback-ôóíêöèÿ, âûçûâàåìàÿ ïðè èíèöèàëèçàöèè óñòàíîâêè
//-----------------------------------------------------------------------------
function InitializeSetup: Boolean;
var
  V: Integer;
  iResultCode: Integer;
  sUnInstallString: string;
begin
  Result := True; // in case when no previous version is found
  if RegValueExists(HKEY_LOCAL_MACHINE,'Software\Microsoft\Windows\CurrentVersion\Uninstall\{667DDB08-76CB-4DB6-B35D-D4B48F4AD8CB}_is1', 'UninstallString') then  //Your App GUID/ID
  begin
    V := MsgBox(ExpandConstant('Hey! An old version of app was detected. Do you want to uninstall it?'), mbInformation, MB_YESNO); //Custom Message if App installed
    if V = IDYES then
    begin
      sUnInstallString := GetUninstallString();
      sUnInstallString :=  RemoveQuotes(sUnInstallString);
      Exec(ExpandConstant(sUnInstallString), '', '', SW_SHOW, ewWaitUntilTerminated, iResultCode);
      Result := True; //if you want to proceed after uninstall
                //Exit; //if you want to quit after uninstall
    end
    else
      Result := False; //when older version present and not uninstalled
  end;

 // Åñëè íåò òåðáóåìîé âåðñèè .NET âûâîäèì ñîîáùåíèå î òîì, ÷òî èíñòàëëÿòîð
  // ïîïûòàåòñÿ óñòàíîâèòü å¸ íà äàííûé êîìïüþòåð
  if not IsDotNetDetected('v4.0 Full Profile', 0) then
    begin
      MsgBox('{#Name} requires Microsoft .NET Framework 4.0 Full Profile.'#13#13
             'The installer will attempt to install it', mbInformation, MB_OK);
    end; 
end;



[Run]
Filename: {tmp}\dotNetFx40_Full_x86_x64.exe; Parameters: "/q:a /c:""install /l /q"""; Check: not IsRequiredDotNetDetected; StatusMsg: Microsoft Framework 4.0 is installed. Please wait...



