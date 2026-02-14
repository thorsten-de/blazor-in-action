export function initialize(hostElement, routeMapComponent) {
  hostElement.map = L.map(hostElement).setView([51.7, 0.1], 7);

  L.tileLayer("https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png", {
    attribution:
      '&copy;<a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
    maxZoom: 18,
    opacity: 0.75,
  }).addTo(hostElement.map);

  hostElement.waypoints = [];
  hostElement.lines = [];

  hostElement.map.on("click", (e) => {
    let waypoint = L.marker(e.latlng);
    waypoint.addTo(hostElement.map);
    hostElement.waypoints.push(waypoint);
    let line = L.polyline(
      hostElement.waypoints.map((m) => m.getLatLng()),
      { color: "var(--brand)" },
    ).addTo(hostElement.map);
    hostElement.lines.push(line);

    routeMapComponent.invokeMethodAsync("WaypointAdded", e.latlng.lat, e.latlng.lng);
  });
}

export function deleteLastWaypoint(hostElement) {
  if (hostElement.waypoints.length > 0) {
    let lastWaypoint = hostElement.waypoints.pop();
    hostElement.map.removeLayer(lastWaypoint);

    if (hostElement.lines.length > 0) {
      let lastLine = hostElement.lines.pop();
      lastLine.remove(hostElement.map);

      let pos = lastWaypoint.getLatLng();
      return `Deleted waypoint with latitude ${pos.lat} and longitude ${pos.lng}`;
    }
  }
}