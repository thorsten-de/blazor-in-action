export function initialize(hostElemnt) {
  hostElemnt.map = L.map(hostElemnt).setView([51.7, 0.1], 3);

  L.tileLayer("https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png", {
    attribution:
      '&copy;<a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
    maxZoom: 18,
    opacity: 0.75,
  }).addTo(hostElement.map);

  hostElemnt.waypoints = [];
  hostElemnt.lines = [];

  hostElemnt.map.on("click", (e) => {
    let waypoint = L.marker(e.latlng);
    waypoint.addTo(hostElemnt.map);
    hostElemnt.waypoints.push(waypoint);
    let line = L.polyline(
      hostElemnt.waypoints.map((m) => m.getLatLng()),
      { color: "var(--brand)" },
    ).addTo(hostElemnt.map);
    hostElemnt.lines.push(line);
  });
}
