const darkModeToggle = document.getElementById('dark-mode-check');
const lightModeToggle = document.getElementById('light-mode-check');
const defaultSideToggle = document.getElementById('default-check');
const darkSideToggle = document.getElementById('dark-check');
const lightSideToggle = document.getElementById('light-check');

const layoutConfigElement = document.querySelector("[data-layout-config]");
const bodyElement = document.body;

function toggleDarkMode() {
    const isDarkMode = bodyElement.classList.contains('darkMode');
    const currentConfig = JSON.parse(layoutConfigElement.getAttribute("data-layout-config"));

    bodyElement.classList.add('darkMode');
    currentConfig.darkMode = true;
    localStorage.setItem('darkMode', 'true');

    layoutConfigElement.setAttribute("data-layout-config", JSON.stringify(currentConfig));

}

function toggleLightMode() {
    const isDarkMode = bodyElement.classList.contains('darkMode');
    const currentConfig = JSON.parse(layoutConfigElement.getAttribute("data-layout-config"));

    bodyElement.classList.add('darkMode');
    currentConfig.darkMode = false;
    localStorage.setItem('darkMode', 'false');

    layoutConfigElement.setAttribute("data-layout-config", JSON.stringify(currentConfig));

}

function toggleDefaultSide() {
    const isDarkMode = bodyElement.classList.contains('leftSideBarTheme');
    const currentConfig = JSON.parse(layoutConfigElement.getAttribute("data-layout-config"));

    bodyElement.classList.add('leftSideBarTheme');
    currentConfig.leftSideBarTheme = "default";
    localStorage.setItem('leftSideBarTheme', 'default');

    layoutConfigElement.setAttribute("data-layout-config", JSON.stringify(currentConfig));

}

function toggleDarkSide() {
    const isDarkMode = bodyElement.classList.contains('leftSideBarTheme');
    const currentConfig = JSON.parse(layoutConfigElement.getAttribute("data-layout-config"));

    bodyElement.classList.add('leftSideBarTheme');
    currentConfig.leftSideBarTheme = "dark";
    localStorage.setItem('leftSideBarTheme', 'dark');

    layoutConfigElement.setAttribute("data-layout-config", JSON.stringify(currentConfig));

}

function toggleLightSide() {
    const isDarkMode = bodyElement.classList.contains('leftSideBarTheme');
    const currentConfig = JSON.parse(layoutConfigElement.getAttribute("data-layout-config"));

    bodyElement.classList.add('leftSideBarTheme');
    currentConfig.leftSideBarTheme = "light";
    localStorage.setItem('leftSideBarTheme', 'light');

    layoutConfigElement.setAttribute("data-layout-config", JSON.stringify(currentConfig));

}

darkModeToggle.addEventListener('change', toggleDarkMode);
lightModeToggle.addEventListener('change', toggleLightMode);
defaultSideToggle.addEventListener('change', toggleDefaultSide);
darkSideToggle.addEventListener('change', toggleDarkSide);
lightSideToggle.addEventListener('change', toggleLightSide);

// Check local storage and set checkbox and data-layout-config on page load
const darkModePreference = localStorage.getItem('darkMode');
if (darkModePreference === 'true') {
    darkModeToggle.checked = true;
    bodyElement.classList.add('darkMode');
    const currentConfig = JSON.parse(layoutConfigElement.getAttribute("data-layout-config"));
    currentConfig.darkMode = true;
    layoutConfigElement.setAttribute("data-layout-config", JSON.stringify(currentConfig));
}
else {
    darkModeToggle.checked = false;
    bodyElement.classList.add('darkMode');
    const currentConfig = JSON.parse(layoutConfigElement.getAttribute("data-layout-config"));
    currentConfig.darkMode = false;
    layoutConfigElement.setAttribute("data-layout-config", JSON.stringify(currentConfig));
}

const sideBarPreference = localStorage.getItem('leftSideBarTheme');
if (sideBarPreference === 'default') {
    defaultSideToggle.checked = true;
    bodyElement.classList.add('leftSideBarTheme');
    const currentConfig = JSON.parse(layoutConfigElement.getAttribute("data-layout-config"));
    currentConfig.leftSideBarTheme = "default";
    layoutConfigElement.setAttribute("data-layout-config", JSON.stringify(currentConfig));
} else if (sideBarPreference === 'dark') {
    darkSideToggle.checked = true;
    bodyElement.classList.add('leftSideBarTheme');
    const currentConfig = JSON.parse(layoutConfigElement.getAttribute("data-layout-config"));
    currentConfig.leftSideBarTheme = "dark";
    layoutConfigElement.setAttribute("data-layout-config", JSON.stringify(currentConfig));
} else if (sideBarPreference === 'light') {
    lightSideToggle.checked = true;
    bodyElement.classList.add('leftSideBarTheme');
    const currentConfig = JSON.parse(layoutConfigElement.getAttribute("data-layout-config"));
    currentConfig.leftSideBarTheme = "light";
    layoutConfigElement.setAttribute("data-layout-config", JSON.stringify(currentConfig));
}
