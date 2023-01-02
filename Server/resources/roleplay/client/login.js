import * as alt from 'alt';
import * as game from 'natives';
import { showCursor, fadeScreenOut, fadeScreenIn, loadIPL, setClothes } from './utilities.js';

let loginBrowser = null,
    loginCamera = null,
    charcreatorPedHandle = null,
    charcreatorModelHash = null;
function createLoginBrowser() {
    if (loginBrowser != null) return;
    loginCamera = game.createCamWithParams("DEFAULT_SCRIPTED_CAMERA", 602, 726, 310, 0, 0, 140, 50, true, 2);
    game.setCamActive(loginCamera, true);
	game.renderScriptCams(true, false, 0, true, false, 0);
    fadeScreenIn(500);
    game.displayRadar(false);
    game.freezeEntityPosition(alt.Player.local.scriptID, true);
    showCursor(true);
    alt.toggleGameControls(false);
	alt.setWatermarkPosition(3);

    loginBrowser = new alt.WebView("http://resource/client/cef/login/index.html");
    loginBrowser.focus();
    loginBrowser.on("Client:Login:cefReady", () => {
        alt.setTimeout(() => {
            if (alt.LocalStorage.get("username")) loginBrowser.emit("CEF:Login:setStorage", alt.LocalStorage.get("username"), alt.LocalStorage.get("password"));
        }, 500);
    });

	loginBrowser.on("Client:Register:sendRegisterDataToServer", (name, password, passwordrepeat, alphakey) => {
        alt.emitServer("Server:Register:RegisterPlayer", name, password, passwordrepeat, alphakey);
    });
	
	loginBrowser.on("Client:Login:changePassword", (password, passwordrepeat) => {
        alt.emitServer("Server:Login:ChangePassword", password, passwordrepeat);
    });
	
    loginBrowser.on("Client:Login:ValidataLoginCredentials", (name, password) => {
        alt.LocalStorage.set("username", name);
        alt.LocalStorage.set("password", password);
        alt.LocalStorage.save();
        alt.emitServer("Server:Login:ValidateLoginCredentials", name, password);
    });
}

function switchToCharSelect(existSkin, typ) {
    if (loginBrowser == null) return;
    if (typ == 0){
        game.switchOutPlayer(alt.Player.local.scriptID, 0, parseInt(1));
        loginBrowser.emit("CEF:Login:fadeOut");
    }
	else if (typ == 1) {
		loginBrowser.emit("CEF:Charcreator:fadeOut");
		game.switchOutPlayer(alt.Player.local.scriptID, 0, parseInt(1));
	}
    alt.setTimeout(() => {
        if (loginBrowser != null) loginBrowser.destroy();
        loginBrowser = null;
        loginBrowser = new alt.WebView("http://resource/client/cef/charselector/index.html");
        loginBrowser.on("Client:Charselector:cefReady", () => {
            alt.setTimeout(() => {
                if (existSkin) openCharCreatorSceneSelect();
                else openCharCreatorGenderSelect();
            }, 250);
        });

        loginBrowser.on("Client:Charselector:spawnCharacter", () => {
            fadeScreenOut(500);
            alt.setTimeout(() => {
                destroyLoginBrowser();
                destroyLoginCam();
            }, 500);
        });
        loginBrowser.focus();
        destroyLoginCam();
		alt.emitServer("Server:Player:setPos", parseFloat(-1033.4637), parseFloat(-2728.0615), parseFloat(20.164062));
        alt.setTimeout(() => {
            game.switchInPlayer(alt.Player.local.scriptID);
        }, 1500);
    }, 1600);
}

function openCharCreatorSceneSelect() {
    alt.setTimeout(() => {
        alt.emitServer("Server:Charselector:loadCharacter");
    }, 1000);
}

function openCharCreatorGenderSelect() {
    fadeScreenOut(1000);
    alt.setTimeout(() => {
        alt.emitServer("Server:Player:setPos", parseFloat(402.778), parseFloat(-996.9758), parseFloat(-98));
    }, 1000);
    alt.setTimeout(() => {
        destroyLoginBrowser();
        fadeScreenIn(1000);
        if (loginBrowser != null) return;
        loginBrowser = new alt.WebView("http://resource/client/cef/charselector/index.html");
        loginBrowser.focus();
        loginBrowser.on("Client:Charselector:cefReady", () => {
            alt.setTimeout(() => {
                fadeScreenOut(500);
                destroyLoginCam();
                alt.emitServer("Server:Charcreator:prepare");
                createLoginCam(402.85, -999, -98.4, 358);
                fadeScreenIn(1000);
                loginBrowser.emit("CEF:Charcreator:openCreator");
            }, 250);
        });

        loginBrowser.on("Client:Charcreator:UpdateHeadOverlays", (headoverlaysarray) => {
            let headoverlays = JSON.parse(headoverlaysarray);
            game.setPedHeadOverlayColor(charcreatorPedHandle, 1, 1, parseInt(headoverlays[2][1]), 1);
            game.setPedHeadOverlayColor(charcreatorPedHandle, 2, 1, parseInt(headoverlays[2][2]), 1);
            game.setPedHeadOverlayColor(charcreatorPedHandle, 5, 2, parseInt(headoverlays[2][5]), 1);
            game.setPedHeadOverlayColor(charcreatorPedHandle, 8, 2, parseInt(headoverlays[2][8]), 1);
            game.setPedHeadOverlayColor(charcreatorPedHandle, 10, 1, parseInt(headoverlays[2][10]), 1);
            game.setPedEyeColor(charcreatorPedHandle, parseInt(headoverlays[0][14]));
            game.setPedHeadOverlay(charcreatorPedHandle, 0, parseInt(headoverlays[0][0]), parseInt(headoverlays[1][0]));
            game.setPedHeadOverlay(charcreatorPedHandle, 1, parseInt(headoverlays[0][1]), parseFloat(headoverlays[1][1]));
            game.setPedHeadOverlay(charcreatorPedHandle, 2, parseInt(headoverlays[0][2]), parseFloat(headoverlays[1][2]));
            game.setPedHeadOverlay(charcreatorPedHandle, 3, parseInt(headoverlays[0][3]), parseInt(headoverlays[1][3]));
            game.setPedHeadOverlay(charcreatorPedHandle, 4, parseInt(headoverlays[0][4]), parseInt(headoverlays[1][4]));
            game.setPedHeadOverlay(charcreatorPedHandle, 5, parseInt(headoverlays[0][5]), parseInt(headoverlays[1][5]));
            game.setPedHeadOverlay(charcreatorPedHandle, 6, parseInt(headoverlays[0][6]), parseInt(headoverlays[1][6]));
            game.setPedHeadOverlay(charcreatorPedHandle, 7, parseInt(headoverlays[0][7]), parseInt(headoverlays[1][7]));
            game.setPedHeadOverlay(charcreatorPedHandle, 8, parseInt(headoverlays[0][8]), parseInt(headoverlays[1][8]));
            game.setPedHeadOverlay(charcreatorPedHandle, 9, parseInt(headoverlays[0][9]), parseInt(headoverlays[1][9]));
            game.setPedHeadOverlay(charcreatorPedHandle, 10, parseInt(headoverlays[0][10]), parseInt(headoverlays[1][10]));
            game.setPedComponentVariation(charcreatorPedHandle, 2, parseInt(headoverlays[0][13]), 0, 0);
            game.setPedHairColor(charcreatorPedHandle, parseInt(headoverlays[2][13]), parseInt(headoverlays[1][13]));
        });

        loginBrowser.on("Client:Charcreator:UpdateFaceFeature", (facefeaturesdata) => {
            let facefeatures = JSON.parse(facefeaturesdata);

            for (let i = 0; i < 20; i++) {
                game.setPedFaceFeature(charcreatorPedHandle, i, parseFloat(facefeatures[i]));
            }
        });

        loginBrowser.on("Client:Charcreator:UpdateHeadBlends", (headblendsdata) => {
            let headblends = JSON.parse(headblendsdata);
			// game.setPedHeadBlendData(charcreatorPedHandle, headblends[0], headblends[1], 0, headblends[2], headblends[5], 0, parseFloat(headblends[3]), headblends[4], 0, true);
			game.setPedHeadBlendData(charcreatorPedHandle, parseInt(headblends[0]), parseInt(headblends[1]), 0, parseInt(headblends[2]), parseInt(headblends[5]), 0, parseFloat(headblends[3]), parseInt(headblends[4]), 0, true);
        });

        loginBrowser.on("Client:Charcreator:updateCam", (category) => {
            if (category == "face") updateLoginCam(402.8, -997.8, -98.3, 0, 0, 358, 50);
            else updateLoginCam(402.85, -999, -98.4, 0, 0, 358, 50);
        });

        loginBrowser.on("Client:Charcreator:setChar", (gender) => {
            spawnCreatorPed(gender);
        });

        loginBrowser.on("Client:Charcreator:CreateCharacter", (firstname, lastname, birthdate, gender, facefeaturesarray, headblendsdataarray, headoverlaysarray) => {
			alt.emitServer("Server:Charcreator:CreateCharacter", firstname, lastname, birthdate, parseInt(gender), facefeaturesarray, headblendsdataarray, headoverlaysarray);
        });
    }, 1500);
}

alt.onServer("Client:Charcreator:showError", (msg) => {
    if (loginBrowser == null) return;
    loginBrowser.emit("CEF:Charcreator:showError", msg);
});

alt.onServer("Client:Charselector:setCorrectSkin", (facefeaturesarray, headblendsarray, headoverlaysarray) => {
    let facefeatures = JSON.parse(facefeaturesarray);
    let headblends = JSON.parse(headblendsarray);
    let headoverlays = JSON.parse(headoverlaysarray);

    // game.setPedHeadBlendData(alt.Player.local.scriptID, headblends[0], headblends[1], 0, headblends[2], headblends[5], 0, parseFloat(headblends[3]), headblends[4], 0, true);
	game.setPedHeadBlendData(alt.Player.local.scriptID, parseInt(headblends[0]), parseInt(headblends[1]), 0, parseInt(headblends[2]), parseInt(headblends[5]), 0, parseFloat(headblends[3]), parseInt(headblends[4]), 0, true);
    game.setPedHeadOverlayColor(alt.Player.local.scriptID, 1, 1, parseInt(headoverlays[2][1]), 1);
    game.setPedHeadOverlayColor(alt.Player.local.scriptID, 2, 1, parseInt(headoverlays[2][2]), 1);
    game.setPedHeadOverlayColor(alt.Player.local.scriptID, 5, 2, parseInt(headoverlays[2][5]), 1);
    game.setPedHeadOverlayColor(alt.Player.local.scriptID, 8, 2, parseInt(headoverlays[2][8]), 1);
    game.setPedHeadOverlayColor(alt.Player.local.scriptID, 10, 1, parseInt(headoverlays[2][10]), 1);
    game.setPedEyeColor(alt.Player.local.scriptID, parseInt(headoverlays[0][14]));
    game.setPedHeadOverlay(alt.Player.local.scriptID, 0, parseInt(headoverlays[0][0]), parseInt(headoverlays[1][0]));
    game.setPedHeadOverlay(alt.Player.local.scriptID, 1, parseInt(headoverlays[0][1]), parseFloat(headoverlays[1][1]));
    game.setPedHeadOverlay(alt.Player.local.scriptID, 2, parseInt(headoverlays[0][2]), parseFloat(headoverlays[1][2]));
    game.setPedHeadOverlay(alt.Player.local.scriptID, 3, parseInt(headoverlays[0][3]), parseInt(headoverlays[1][3]));
    game.setPedHeadOverlay(alt.Player.local.scriptID, 4, parseInt(headoverlays[0][4]), parseInt(headoverlays[1][4]));
    game.setPedHeadOverlay(alt.Player.local.scriptID, 5, parseInt(headoverlays[0][5]), parseInt(headoverlays[1][5]));
    game.setPedHeadOverlay(alt.Player.local.scriptID, 6, parseInt(headoverlays[0][6]), parseInt(headoverlays[1][6]));
    game.setPedHeadOverlay(alt.Player.local.scriptID, 7, parseInt(headoverlays[0][7]), parseInt(headoverlays[1][7]));
    game.setPedHeadOverlay(alt.Player.local.scriptID, 8, parseInt(headoverlays[0][8]), parseInt(headoverlays[1][8]));
    game.setPedHeadOverlay(alt.Player.local.scriptID, 9, parseInt(headoverlays[0][9]), parseInt(headoverlays[1][9]));
    game.setPedHeadOverlay(alt.Player.local.scriptID, 10, parseInt(headoverlays[0][10]), parseInt(headoverlays[1][10]));
    game.setPedComponentVariation(alt.Player.local.scriptID, 2, parseInt(headoverlays[0][13]), 0, 0);
    game.setPedHairColor(alt.Player.local.scriptID, parseInt(headoverlays[2][13]), parseInt(headoverlays[1][13]));

    for (let i = 0; i < 20; i++) {
        game.setPedFaceFeature(alt.Player.local.scriptID, i, parseFloat(facefeatures[i]));
    }
});

alt.onServer("Client:Charselector:spawnCharacterFinal", () => {
    alt.setTimeout(() => {
        destroyLoginBrowser();
        destroyLoginCam();
    }, 600);

    alt.setTimeout(() => {
        fadeScreenIn(1000);
        game.displayRadar(true);
        game.freezeEntityPosition(alt.Player.local.scriptID, false);
        showCursor(false);
        alt.toggleGameControls(true);
    }, 1500);
});

alt.onServer("Client:Login:destroyBrowser", () => {
    destroyLoginBrowser();
    showCursor(false);
    fadeScreenOut(450);
    alt.setTimeout(() => {
        destroyLoginCam();
        game.displayRadar(true);
        game.freezeEntityPosition(alt.Player.local.scriptID, false);
        game.hideHudComponentThisFrame(9)
        alt.toggleGameControls(true);
    }, 500);
});

alt.onServer("Client:Login:showArea", (type) => {
    if (loginBrowser != null) {
        loginBrowser.emit("Login:showArea", type);
    }
});

function openCharCreator() {
    if (loginBrowser == null || gender == undefined) return;
    alt.emitServer("Server:Charcreator:prepare");
    alt.setTimeout(() => {
        createLoginCam(402.85, -999, -98.4, 358);
        fadeScreenIn(1000);
    }, 1500);
}

function spawnCreatorPed(gender) { //gender (0 - male | 1 - female)
    if (gender == 0) charcreatorModelHash = game.getHashKey('mp_m_freemode_01');
    else if (gender == 1) charcreatorModelHash = game.getHashKey('mp_f_freemode_01');
    else return;
    game.requestModel(charcreatorModelHash);
    let interval = alt.setInterval(() => {
        if (game.hasModelLoaded(charcreatorModelHash)) {
            alt.clearInterval(interval);
            game.deletePed(charcreatorPedHandle)
            charcreatorPedHandle = game.createPed(4, charcreatorModelHash, 402.778, -996.9758, -100.01465, 0, false, true);
            game.setEntityHeading(charcreatorPedHandle, 180.0);
            game.setEntityInvincible(charcreatorPedHandle, true);
            game.disablePedPainAudio(charcreatorPedHandle, true);
            game.freezeEntityPosition(charcreatorPedHandle, true);
            game.taskSetBlockingOfNonTemporaryEvents(charcreatorPedHandle, true);


            setClothes(charcreatorPedHandle, 11, 15, 0);
            if (gender == 0) setClothes(charcreatorPedHandle, 8, 57, 0);
            else if (gender == 1) setClothes(charcreatorPedHandle, 8, 3, 0);
            setClothes(charcreatorPedHandle, 3, 15, 0);
        }
    }, 0);
}

function destroyLoginBrowser() {
    if (loginBrowser != null) loginBrowser.destroy();
    loginBrowser = null;
}

function destroyLoginCam() {
    game.renderScriptCams(false, false, 0, true, false, 0);
    if (loginCamera != null) {
        game.setCamActive(loginCamera, false);
        game.destroyCam(loginCamera, true);
        loginCamera = null;
    }
}

function updateLoginCam(posX, posY, posZ, rotX, rotY, rotZ, fov) {
    if (loginCamera == null) return;
    game.setCamCoord(loginCamera, posX, posY, posZ);
    game.setCamRot(loginCamera, rotX, rotY, rotZ, 2);
    game.setCamFov(loginCamera, fov);
    game.setCamActive(loginCamera, true);
    game.renderScriptCams(true, false, 0, true, false, 0);
}

function createLoginCam(x, y, z, rot) {
    if (loginCamera != null) game.destroyCam(loginCamera, true);
    loginCamera = game.createCamWithParams("DEFAULT_SCRIPTED_CAMERA", x, y, z, 0, 0, rot, 50, true, 2);
    game.setCamActive(loginCamera, true);
    game.renderScriptCams(true, false, 0, true, false, 0);
}

alt.on("connectionComplete", () => {
    loadallIPLsAndInteriors();
	fadeScreenOut(100);
    alt.setTimeout(() => {
        createLoginBrowser();
    }, 3500);
});

alt.onServer("Client:Login:loginSuccess", (existSkin, typ) => {
    switchToCharSelect(existSkin, typ);
    if (typ == 1 && charcreatorPedHandle != null) {
        game.deletePed(charcreatorPedHandle);
        charcreatorPedHandle = null;
    }
});

alt.onServer("Client:Login:showError", (msg) => {
    if (loginBrowser == null) return;
    loginBrowser.emit("CEF:Login:showError", msg);
});

function loadallIPLsAndInteriors() {
	alt.requestIpl("rc12b_default"); //Pillbox Hill Hospital
	
	alt.requestIpl('shr_int'); //Premium Deluxe Motorsports
    game.activateInteriorEntitySet(game.getInteriorAtCoordsWithType(-38.62, -1099.01, 27.31, 'v_carshowroom'), 'csr_beforeMission'); //Premium Deluxe Motorsports
    game.activateInteriorEntitySet(game.getInteriorAtCoordsWithType(-38.62, -1099.01, 27.31, 'v_carshowroom'), 'shutter_closed'); //Premium Deluxe Motorsports

}

alt.everyTick(() => {
	game.hideHudComponentThisFrame(6);
	game.hideHudComponentThisFrame(7);
	game.hideHudComponentThisFrame(8);
	game.hideHudComponentThisFrame(9);
});