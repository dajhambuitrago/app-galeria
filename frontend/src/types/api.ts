/**
 * Respuesta de autenticación con token y roles.
 */
export interface AuthResponseDto {
  token: string
  email: string
  roles: string[]
}

/**
 * Representa una categoría disponible.
 */
export interface CategoryDto {
  id: string
  name: string
}

/**
 * Representa un medio de la galería.
 */
export interface MediaDto {
  id: string
  title: string
  url: string
  description: string
  type: number
  categoryName: string
}

/**
 * Representa un favorito del usuario.
 */
export interface FavoriteDto {
  mediaItemId: string
  title: string
  url: string
}

/**
 * Representa los datos del slideshow.
 */
export interface SlideshowDto {
  slideshowDurationSeconds: number
  totalItems: number
  effectiveOrder: string
  items: MediaDto[]
}

/**
 * Representa la configuración del slideshow.
 */
export interface SettingDto {
  slideshowDurationSeconds: number
}
