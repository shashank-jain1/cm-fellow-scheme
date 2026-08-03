interface GeoLocation {
  latitude: number;
  longitude: number;
}

interface Props {
  location: GeoLocation | null;
  locationLoading: boolean;
  locationError: string | null;
}

export default function FaceMatchResult({ location, locationLoading, locationError }: Props) {
  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: 8 }}>
      {locationLoading && (
        <div style={{ display: 'flex', alignItems: 'center', gap: 8, color: 'var(--text-secondary)', fontSize: 13 }}>
          <i className="pi pi-spin pi-map-marker" />
          <span>Fetching GPS coordinates...</span>
        </div>
      )}
      {locationError && (
        <div style={{ display: 'flex', alignItems: 'center', gap: 8, color: 'var(--red-600)', fontSize: 13 }}>
          <i className="pi pi-exclamation-triangle" />
          <span>{locationError}</span>
        </div>
      )}
      {location && (
        <div style={{ display: 'flex', alignItems: 'center', gap: 8, padding: '8px 12px', background: 'var(--emerald-50)', borderRadius: 8, fontSize: 13, color: 'var(--emerald-700)' }}>
          <i className="pi pi-map-marker" />
          <span>Lat: {location.latitude.toFixed(6)}, Long: {location.longitude.toFixed(6)}</span>
        </div>
      )}
    </div>
  );
}
