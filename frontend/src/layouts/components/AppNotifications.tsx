import { useState, useRef, useEffect } from 'react';
import { useQuery } from '@tanstack/react-query';
import ApiService from '../../services/ApiService';

interface NotificationItem {
  notificationId: number;
  channel: string;
  subject: string;
  body: string;
  status: string;
  sentOn: string;
}

function getNotifIcon(channel: string) {
  if (channel === 'SMS') return 'pi pi-mobile';
  if (channel === 'Email') return 'pi pi-envelope';
  return 'pi pi-bell';
}

function getNotifColor(status: string) {
  if (status === 'Failed') return 'var(--danger)';
  return 'var(--info)';
}

function timeAgo(dateStr: string) {
  const diff = Date.now() - new Date(dateStr).getTime();
  const mins = Math.floor(diff / 60000);
  if (mins < 1) return 'Just now';
  if (mins < 60) return `${mins}m ago`;
  const hrs = Math.floor(mins / 60);
  if (hrs < 24) return `${hrs}h ago`;
  const days = Math.floor(hrs / 24);
  return `${days}d ago`;
}

export default function AppNotifications() {
  const [showNotifications, setShowNotifications] = useState(false);
  const notifRef = useRef<HTMLDivElement>(null);

  const { data: notifications = [] } = useQuery({
    queryKey: ['notifications'],
    queryFn: () => ApiService.get<NotificationItem[]>('notifications').then((r) => r.data ?? []),
    refetchInterval: 30000,
  });

  const unreadCount = notifications.filter((n) => n.status === 'Sent').length;

  useEffect(() => {
    function handleClickOutside(e: MouseEvent) {
      if (notifRef.current && !notifRef.current.contains(e.target as Node)) {
        setShowNotifications(false);
      }
    }
    document.addEventListener('mousedown', handleClickOutside);
    return () => document.removeEventListener('mousedown', handleClickOutside);
  }, []);

  return (
    <div ref={notifRef} style={{ position: 'relative' }}>
      <button
        className="header-icon-btn"
        title="Notifications"
        onClick={() => setShowNotifications(!showNotifications)}
      >
        <i className="pi pi-bell" />
        {unreadCount > 0 && <span className="notification-dot" />}
      </button>

      {showNotifications && (
        <div className="header-dropdown-menu notif-dropdown">
          <div className="dropdown-header">
            <span style={{ fontWeight: 700, fontSize: 14 }}>Notifications</span>
            {unreadCount > 0 && <span className="badge badge-info">{unreadCount} New</span>}
          </div>
          <div className="notif-list">
            {notifications.length === 0 ? (
              <div style={{ padding: 20, textAlign: 'center', color: 'var(--text-muted)', fontSize: 13 }}>
                No notifications yet
              </div>
            ) : (
              notifications.slice(0, 10).map((n) => (
                <div key={n.notificationId} className={`notif-item ${n.status === 'Sent' ? 'unread' : ''}`}>
                  <div className="notif-icon-box" style={{ background: 'var(--accent-light)', color: getNotifColor(n.status) }}>
                    <i className={getNotifIcon(n.channel)} />
                  </div>
                  <div style={{ flex: 1 }}>
                    <div style={{ fontSize: 13, fontWeight: n.status === 'Sent' ? 600 : 400, color: 'var(--text-heading)' }}>
                      {n.subject}
                    </div>
                    <div style={{ fontSize: 11, color: 'var(--text-muted)', marginTop: 2 }}>{timeAgo(n.sentOn)}</div>
                  </div>
                </div>
              ))
            )}
          </div>
          <div className="dropdown-footer">
            <button className="btn-link" onClick={() => setShowNotifications(false)}>View All Notifications</button>
          </div>
        </div>
      )}
    </div>
  );
}
